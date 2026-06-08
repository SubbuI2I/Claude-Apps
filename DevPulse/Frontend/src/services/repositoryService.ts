import apiClient from './api';

export const repositoryService = {
  getRepositories: async () => {
    const response = await apiClient.get('/repositories');
    return response.data;
  },

  connectRepository: async (owner: string, name: string) => {
    const response = await apiClient.post('/repositories/connect', {
      owner,
      name,
    });
    return response.data;
  },

  getRepository: async (repoId: string) => {
    const response = await apiClient.get(`/repositories/${repoId}`);
    return response.data;
  },

  disconnectRepository: async (repoId: string) => {
    const response = await apiClient.delete(`/repositories/${repoId}`);
    return response.data;
  },
};
