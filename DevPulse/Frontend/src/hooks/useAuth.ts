import { useState, useEffect } from 'react';
import { authService } from '../services/authService';

export const useAuth = () => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const token = authService.getToken();
    setIsAuthenticated(!!token);
    setLoading(false);
  }, []);

  const login = async (email: string, password: string) => {
    const result = await authService.login(email, password);
    authService.setToken(result.token);
    setIsAuthenticated(true);
    return result;
  };

  const register = async (
    email: string,
    password: string,
    fullName?: string
  ) => {
    const result = await authService.register(email, password, fullName);
    authService.setToken(result.token);
    setIsAuthenticated(true);
    return result;
  };

  const logout = () => {
    authService.logout();
    setIsAuthenticated(false);
  };

  return {
    isAuthenticated,
    loading,
    login,
    register,
    logout,
  };
};
