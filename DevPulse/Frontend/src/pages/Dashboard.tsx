import { useState, useEffect } from 'react';
import { metricsService } from '../services/metricsService';
import { DashboardMetrics } from '../types';

export default function Dashboard() {
  const [metrics, setMetrics] = useState<DashboardMetrics | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchMetrics();
  }, []);

  const fetchMetrics = async () => {
    try {
      const data = await metricsService.getDashboardMetrics();
      setMetrics(data);
    } catch (error) {
      console.error('Failed to fetch metrics:', error);
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return <div className="p-8">Loading...</div>;
  }

  if (!metrics) {
    return <div className="p-8">Failed to load metrics</div>;
  }

  const metricCards = [
    { label: 'Repositories', value: metrics.totalRepositories, color: 'bg-blue-100' },
    { label: 'Commits', value: metrics.totalCommits, color: 'bg-green-100' },
    { label: 'Pull Requests', value: metrics.totalPullRequests, color: 'bg-yellow-100' },
    { label: 'Issues', value: metrics.totalIssues, color: 'bg-red-100' },
    { label: 'Stars', value: metrics.totalStars, color: 'bg-purple-100' },
  ];

  return (
    <div className="p-8">
      <h1 className="text-3xl font-bold mb-8">Dashboard</h1>

      <div className="grid grid-cols-1 md:grid-cols-5 gap-4">
        {metricCards.map((card) => (
          <div key={card.label} className={`${card.color} rounded-lg p-6`}>
            <p className="text-gray-600 text-sm font-semibold">{card.label}</p>
            <p className="text-3xl font-bold mt-2">{card.value}</p>
          </div>
        ))}
      </div>
    </div>
  );
}
