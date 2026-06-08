import apiClient from './api';

export const metricsService = {
  getDashboardMetrics: async () => {
    const response = await apiClient.get('/metrics/dashboard');
    return response.data;
  },

  getRepositoryMetrics: async (
    repoId: string,
    from?: Date,
    to?: Date
  ) => {
    const params = new URLSearchParams();
    if (from) params.append('from', from.toISOString());
    if (to) params.append('to', to.toISOString());

    const response = await apiClient.get(
      `/metrics/${repoId}?${params.toString()}`
    );
    return response.data;
  },

  getLatestMetric: async (repoId: string) => {
    const response = await apiClient.get(`/metrics/${repoId}/latest`);
    return response.data;
  },

  recordMetrics: async (
    repoId: string,
    data: {
      commitCount: number;
      pullRequestCount: number;
      issueCount: number;
      starCount: number;
      forksCount: number;
      averagePRMergeTimeHours?: number;
    }
  ) => {
    const response = await apiClient.post(`/metrics/${repoId}`, data);
    return response.data;
  },
};
