import { useState, useEffect } from 'react';
import { repositoryService } from '../services/repositoryService';
import { Repository } from '../types';
import { RepositoryCard } from '../components/RepositoryCard';

export default function Repositories() {
  const [repos, setRepos] = useState<Repository[]>([]);
  const [owner, setOwner] = useState('');
  const [name, setName] = useState('');
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');

  useEffect(() => {
    loadRepositories();
  }, []);

  const loadRepositories = async () => {
    setLoading(true);
    setError('');

    try {
      const result = await repositoryService.getRepositories();
      setRepos(result);
    } catch (err) {
      setError('Unable to load repositories. Please try again.');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const handleConnect = async (event: React.FormEvent) => {
    event.preventDefault();
    setError('');
    setSuccess('');

    if (!owner.trim() || !name.trim()) {
      setError('Owner and repository name are required.');
      return;
    }

    try {
      await repositoryService.connectRepository(owner.trim(), name.trim());
      setSuccess('Repository connected successfully.');
      setOwner('');
      setName('');
      await loadRepositories();
    } catch (err) {
      setError('Failed to connect repository. Please check the name and try again.');
      console.error(err);
    }
  };

  const handleDisconnect = async (repoId: string) => {
    setError('');
    setSuccess('');

    try {
      await repositoryService.disconnectRepository(repoId);
      setSuccess('Repository disconnected successfully.');
      await loadRepositories();
    } catch (err) {
      setError('Failed to disconnect repository.');
      console.error(err);
    }
  };

  const handleView = (repo: Repository) => {
    window.open(repo.repositoryUrl, '_blank');
  };

  return (
    <div className="p-8">
      <h1 className="text-3xl font-bold mb-6">Repositories</h1>

      <div className="bg-white rounded-lg shadow p-6 mb-8">
        <h2 className="text-xl font-semibold mb-4">Connect a GitHub Repository</h2>
        {error && <div className="bg-red-100 text-red-700 p-3 rounded mb-4">{error}</div>}
        {success && <div className="bg-green-100 text-green-700 p-3 rounded mb-4">{success}</div>}

        <form onSubmit={handleConnect} className="grid gap-4 sm:grid-cols-2">
          <div>
            <label className="block text-gray-700 font-semibold mb-2">Owner</label>
            <input
              type="text"
              value={owner}
              onChange={(e) => setOwner(e.target.value)}
              className="w-full px-3 py-2 border border-gray-300 rounded"
              placeholder="github-owner"
            />
          </div>

          <div>
            <label className="block text-gray-700 font-semibold mb-2">Repository Name</label>
            <input
              type="text"
              value={name}
              onChange={(e) => setName(e.target.value)}
              className="w-full px-3 py-2 border border-gray-300 rounded"
              placeholder="repository-name"
            />
          </div>

          <div className="sm:col-span-2">
            <button
              type="submit"
              className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
            >
              Connect Repository
            </button>
          </div>
        </form>
      </div>

      <div className="bg-white rounded-lg shadow p-6">
        <div className="flex items-center justify-between mb-6">
          <h2 className="text-xl font-semibold">Connected Repositories</h2>
          <button
            onClick={loadRepositories}
            className="text-sm text-blue-600 hover:underline"
          >
            Refresh
          </button>
        </div>

        {loading ? (
          <div>Loading repositories...</div>
        ) : repos.length === 0 ? (
          <div className="text-gray-600">No connected repositories yet.</div>
        ) : (
          repos.map((repo) => (
            <RepositoryCard
              key={repo.id}
              repo={repo}
              onView={handleView}
              onDisconnect={handleDisconnect}
            />
          ))
        )}
      </div>
    </div>
  );
}
