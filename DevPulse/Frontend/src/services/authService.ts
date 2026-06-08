import apiClient from './api';

export const authService = {
  register: async (email: string, password: string, fullName?: string) => {
    const response = await apiClient.post('/auth/register', {
      email,
      password,
      fullName,
    });
    return response.data;
  },

  login: async (email: string, password: string) => {
    const response = await apiClient.post('/auth/login', {
      email,
      password,
    });
    return response.data;
  },

  validate: async () => {
    const response = await apiClient.get('/auth/validate');
    return response.data;
  },

  logout: () => {
    localStorage.removeItem('authToken');
  },

  setToken: (token: string) => {
    localStorage.setItem('authToken', token);
  },

  getToken: () => {
    return localStorage.getItem('authToken');
  },
};
