import { Repository } from '../types';

interface RepositoryCardProps {
  repo: Repository;
  onView: (repo: Repository) => void;
  onDisconnect: (repoId: string) => void;
}

export const RepositoryCard = ({
  repo,
  onView,
  onDisconnect,
}: RepositoryCardProps) => {
  return (
    <div className="bg-white rounded-lg shadow p-6 mb-4">
      <div className="flex justify-between items-start">
        <div>
          <h3 className="text-xl font-semibold">
            <a
              href={repo.repositoryUrl}
              target="_blank"
              rel="noopener noreferrer"
              className="text-blue-600 hover:underline"
            >
              {repo.owner}/{repo.name}
            </a>
          </h3>
          <p className="text-gray-600 text-sm mt-1">{repo.description}</p>
          <p className="text-gray-500 text-xs mt-2">
            Connected: {new Date(repo.connectedAt).toLocaleDateString()}
          </p>
        </div>
        <div className="flex gap-2">
          <button
            onClick={() => onView(repo)}
            className="px-3 py-2 bg-blue-600 text-white rounded text-sm hover:bg-blue-700"
          >
            View
          </button>
          <button
            onClick={() => onDisconnect(repo.id)}
            className="px-3 py-2 bg-red-600 text-white rounded text-sm hover:bg-red-700"
          >
            Disconnect
          </button>
        </div>
      </div>
    </div>
  );
};
