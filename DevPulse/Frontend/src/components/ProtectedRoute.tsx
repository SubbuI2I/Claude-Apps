import { ReactNode } from 'react';
import { useAuth } from '../hooks/useAuth';
import Login from '../pages/Login';

export const ProtectedRoute = ({ children }: { children: ReactNode }) => {
  const { isAuthenticated, loading } = useAuth();

  if (loading) {
    return <div className="flex justify-center items-center h-screen">Loading...</div>;
  }

  if (!isAuthenticated) {
    return <Login />;
  }

  return <>{children}</>;
};
