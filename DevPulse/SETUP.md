# DevPulse Setup Guide

## Prerequisites Installation

### Windows

#### .NET 8 SDK
- Download from: https://dotnet.microsoft.com/en-us/download/dotnet/8.0
- Run installer and follow prompts
- Verify: `dotnet --version`

#### PostgreSQL
- Download from: https://www.postgresql.org/download/windows/
- Run installer (use default port 5432, user: postgres, password: postgres)
- Verify: `psql --version`

#### Node.js
- Download from: https://nodejs.org/ (LTS version)
- Run installer
- Verify: `node --version` and `npm --version`

#### Git
- Download from: https://git-scm.com/download/win
- Run installer
- Verify: `git --version`

### macOS

```bash
# Using Homebrew
brew install dotnet postgresql node git

# Verify installations
dotnet --version
psql --version
node --version
npm --version
git --version
```

### Linux (Ubuntu/Debian)

```bash
# .NET SDK
wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh --version latest

# PostgreSQL
sudo apt-get update
sudo apt-get install postgresql postgresql-contrib

# Node.js
curl -fsSL https://deb.nodesource.com/setup_18.x | sudo -E bash -
sudo apt-get install -y nodejs

# Git
sudo apt-get install git
```

## Initial Setup

### 1. Clone the Repository
```bash
git clone <repository-url>
cd DevPulse
```

### 2. Setup PostgreSQL Database

Create database:
```bash
psql -U postgres -c "CREATE DATABASE devpulse;"
```

### 3. Backend Configuration

```bash
cd Backend/DevPulse.API
cp appsettings.json appsettings.local.json
```

Edit `appsettings.json`:
- Update PostgreSQL connection string
- Add GitHub Personal Access Token
- Update JWT secret

```bash
# Restore packages
dotnet restore

# Create initial migration
dotnet ef migrations add InitialCreate --project ../DevPulse.Data

# Apply migrations
dotnet ef database update

# Run backend
dotnet run
```

### 4. Frontend Configuration

```bash
cd Frontend
npm install
npm run dev
```

## Running the Application

### Terminal 1: Backend
```bash
cd Backend/DevPulse.API
dotnet run
# API available at https://localhost:7294
```

### Terminal 2: Frontend
```bash
cd Frontend
npm run dev
# Frontend available at http://localhost:3000
```

### Using Docker Compose
```bash
docker-compose up -d
# API: https://localhost:7294
# Frontend: http://localhost:3000
# PostgreSQL: localhost:5432
```

## GitHub Integration

### Get Personal Access Token
1. Go to GitHub Settings → Developer settings → Personal access tokens
2. Click "Generate new token"
3. Select scopes: `repo`, `read:user`, `user:email`
4. Copy token and add to `appsettings.json`

## Troubleshooting

### Port Already in Use
- API (7294): `netstat -ano | findstr :7294` (Windows)
- Frontend (3000): `lsof -i :3000` (macOS/Linux)
- PostgreSQL (5432): `netstat -ano | findstr :5432` (Windows)

### Database Connection Issues
```bash
# Test connection
psql -h localhost -U postgres -d devpulse

# Check PostgreSQL service
sudo systemctl status postgresql  # Linux
brew services list | grep postgres  # macOS
```

### Frontend Build Errors
```bash
cd Frontend
rm -rf node_modules package-lock.json
npm install
npm run dev
```

## Next Steps

1. Register a new user at `http://localhost:3000`
2. Connect a GitHub repository
3. View dashboard metrics
4. Explore the API using Swagger UI at `https://localhost:7294/swagger`

## Architecture Overview

```
User Browser (http://localhost:3000)
        ↓
React Frontend (Vite)
        ↓
Backend API (https://localhost:7294)
        ↓
PostgreSQL Database (localhost:5432)
        ↓
GitHub API
```

## Security Reminders

1. Change default PostgreSQL password
2. Use strong JWT secret in production
3. Never commit sensitive data to git
4. Use environment variables for secrets
5. Update dependencies regularly: `npm audit fix`

## Additional Resources

- .NET Documentation: https://learn.microsoft.com/en-us/dotnet/
- PostgreSQL Docs: https://www.postgresql.org/docs/
- React Docs: https://react.dev/
- GitHub API: https://docs.github.com/en/rest
