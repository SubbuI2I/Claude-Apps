import { BrowserRouter as Router, Routes, Route, Link, useNavigate } from 'react-router-dom';
import { useAuth } from './hooks/useAuth';
import { ProtectedRoute } from './components/ProtectedRoute';
import Login from './pages/Login';
import Dashboard from './pages/Dashboard';
import './index.css';

function App() {
  const { isAuthenticated, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate('/');
  };

  return (
    <div className="min-h-screen bg-gray-50">
      {isAuthenticated && (
        <nav className="bg-white shadow">
          <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
            <div className="flex justify-between h-16">
              <div className="flex items-center">
                <Link to="/dashboard" className="text-xl font-bold text-blue-600">
                  DevPulse
                </Link>
                <div className="hidden md:flex space-x-8 ml-10">
                  <Link to="/dashboard" className="text-gray-700 hover:text-gray-900">
                    Dashboard
                  </Link>
                  <Link to="/repositories" className="text-gray-700 hover:text-gray-900">
                    Repositories
                  </Link>
                </div>
              </div>
              <div className="flex items-center">
                <button
                  onClick={handleLogout}
                  className="text-gray-700 hover:text-gray-900 px-3 py-2"
                >
                  Logout
                </button>
              </div>
            </div>
          </div>
        </nav>
      )}

      <main className={isAuthenticated ? 'max-w-7xl mx-auto' : ''}>
        <Routes>
          <Route path="/" element={isAuthenticated ? <Dashboard /> : <Login />} />
          <Route
            path="/dashboard"
            element={
              <ProtectedRoute>
                <Dashboard />
              </ProtectedRoute>
            }
          />
        </Routes>
      </main>
    </div>
  );
}

export default App;
