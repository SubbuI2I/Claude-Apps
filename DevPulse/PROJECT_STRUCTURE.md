# DevPulse — Complete Project Structure

## 📁 Directory Layout

```
DevPulse/
│
├── 📁 Backend/
│   ├── 📁 DevPulse.API/
│   │   ├── 📁 Controllers/
│   │   │   ├── AuthController.cs
│   │   │   ├── RepositoriesController.cs
│   │   │   └── MetricsController.cs
│   │   ├── 📁 Models/
│   │   │   └── (Request/Response DTOs)
│   │   ├── appsettings.json
│   │   ├── appsettings.Development.json
│   │   ├── Program.cs
│   │   ├── Dockerfile
│   │   └── DevPulse.API.csproj
│   │
│   ├── 📁 DevPulse.Data/
│   │   ├── 📁 Models/
│   │   │   └── DomainModels.cs
│   │   ├── 📁 Migrations/
│   │   │   └── (EF Core migrations)
│   │   ├── DevPulseContext.cs
│   │   └── DevPulse.Data.csproj
│   │
│   ├── 📁 DevPulse.Core/
│   │   ├── 📁 Services/
│   │   │   ├── AuthService.cs
│   │   │   ├── RepositoryService.cs
│   │   │   ├── MetricsService.cs
│   │   │   └── GitHubService.cs
│   │   └── DevPulse.Core.csproj
│   │
│   ├── .gitignore
│   └── DevPulse.sln
│
├── 📁 Frontend/
│   ├── 📁 src/
│   │   ├── 📁 components/
│   │   │   ├── ProtectedRoute.tsx
│   │   │   ├── MetricsChart.tsx
│   │   │   └── RepositoryCard.tsx
│   │   ├── 📁 pages/
│   │   │   ├── Login.tsx
│   │   │   └── Dashboard.tsx
│   │   ├── 📁 services/
│   │   │   ├── api.ts
│   │   │   ├── authService.ts
│   │   │   ├── repositoryService.ts
│   │   │   └── metricsService.ts
│   │   ├── 📁 hooks/
│   │   │   └── useAuth.ts
│   │   ├── 📁 types/
│   │   │   └── index.ts
│   │   ├── App.tsx
│   │   ├── main.tsx
│   │   └── index.css
│   ├── 📁 public/
│   ├── index.html
│   ├── vite.config.ts
│   ├── tsconfig.json
│   ├── tailwind.config.js
│   ├── postcss.config.js
│   ├── package.json
│   ├── Dockerfile
│   ├── nginx.conf
│   ├── .env.development
│   ├── .gitignore
│   └── Dockerfile
│
├── 📁 Docs/
│   ├── API_TESTING.md
│   └── (Additional documentation)
│
├── 📁 .github/
│   └── 📁 workflows/
│       └── ci-cd.yml
│
├── 📁 .vscode/
│   └── extensions.json
│
├── README.md                  # Main project documentation
├── SETUP.md                   # Setup instructions
├── ARCHITECTURE.md            # Architecture overview
├── CONTRIBUTING.md            # Contributing guidelines
├── DevPulse.code-workspace    # VS Code workspace config
├── docker-compose.yml         # Docker Compose configuration
├── .editorconfig              # Editor configuration
├── .gitignore                 # Git ignore rules
├── .env.example               # Environment variables template
└── LICENSE                    # License file
```

## 🎯 Quick Start

### Option 1: Local Development
```bash
# Backend
cd Backend/DevPulse.API
dotnet restore
dotnet ef database update
dotnet run

# Frontend (new terminal)
cd Frontend
npm install
npm run dev
```

### Option 2: Docker
```bash
docker-compose up -d
```

## 📊 Project Statistics

| Metric | Value |
|--------|-------|
| **Backend Projects** | 3 (.API, .Data, .Core) |
| **API Endpoints** | 10+ |
| **Database Tables** | 5 |
| **Frontend Components** | 4+ |
| **Services** | 4 |
| **React Pages** | 2+ |
| **Documentation Files** | 5+ |

## 🔧 Technologies

| Layer | Technologies |
|-------|--------------|
| **Frontend** | React 18, TypeScript, Vite, Tailwind CSS, Recharts, Axios |
| **Backend** | .NET 8, ASP.NET Core, Entity Framework Core, JWT, Octokit |
| **Database** | PostgreSQL 13+ |
| **DevOps** | Docker, Docker Compose, GitHub Actions |

## 📝 Key Files Explained

### Backend Core
- **Program.cs** - Application configuration, service registration, middleware setup
- **DevPulseContext.cs** - EF Core DbContext, database configuration
- **DomainModels.cs** - Entity models (User, Repository, Metric, etc.)
- **AuthService.cs** - Authentication logic, JWT generation
- **RepositoryService.cs** - Repository CRUD operations
- **MetricsService.cs** - Metrics aggregation and calculations
- **GitHubService.cs** - GitHub API integration

### Frontend Core
- **App.tsx** - Main application component, routing setup
- **main.tsx** - React entry point
- **api.ts** - Axios client configuration with interceptors
- **useAuth.ts** - Authentication state management hook
- **Dashboard.tsx** - Main dashboard page with metrics cards
- **Login.tsx** - Authentication form (register/login)

### Configuration
- **appsettings.json** - Database, JWT, GitHub credentials
- **.env.example** - Environment variables template
- **docker-compose.yml** - Multi-container setup
- **vite.config.ts** - Frontend build configuration

## 🚀 Deployment Architecture

```
┌─────────────────────────────────────────────┐
│         Client Browser                       │
│      (http://localhost:3000)                 │
└──────────────────┬──────────────────────────┘
                   │
        ┌──────────▼──────────┐
        │   Nginx Reverse Proxy│
        └──────────┬──────────┘
                   │
    ┌──────────────┼──────────────┐
    │              │              │
┌───▼──┐    ┌─────▼──────┐  ┌───▼────┐
│React │    │ .NET Core  │  │PostgreSQL
│  SPA │    │   API      │  │ Database
└──────┘    └────────────┘  └────────┘
    │              │              │
    └──────────────┴──────────────┘
         Docker Network
```

## 📋 API Endpoints Summary

| Method | Endpoint | Purpose |
|--------|----------|---------|
| POST | `/api/auth/register` | Register user |
| POST | `/api/auth/login` | Login user |
| GET | `/api/auth/validate` | Validate token |
| GET | `/api/repositories` | List user repos |
| POST | `/api/repositories/connect` | Connect GitHub repo |
| DELETE | `/api/repositories/{id}` | Disconnect repo |
| GET | `/api/metrics/dashboard` | Dashboard stats |
| GET | `/api/metrics/{repoId}` | Repo metrics |
| POST | `/api/metrics/{repoId}` | Record metrics |

## 🔐 Security Features

✅ JWT Authentication with expiration
✅ Password hashing with BCrypt
✅ CORS protection
✅ Secure database connection
✅ Environment variable protection
✅ Token validation on protected routes

## 📚 Documentation Guide

- **README.md** - Project overview and features
- **SETUP.md** - Installation and setup instructions
- **ARCHITECTURE.md** - System design and patterns
- **CONTRIBUTING.md** - Contributing guidelines
- **API_TESTING.md** - API endpoint testing guide

## 🎓 Learning Resources

### Backend (C#/.NET)
- Microsoft Learn: https://learn.microsoft.com/en-us/dotnet/
- Entity Framework Core: https://learn.microsoft.com/en-us/ef/core/

### Frontend (React/TypeScript)
- React Documentation: https://react.dev/
- TypeScript Handbook: https://www.typescriptlang.org/docs/

### GitHub API
- GitHub REST API: https://docs.github.com/en/rest
- Octokit.net: https://github.com/octokit/octokit.net

## 🐛 Troubleshooting

See [SETUP.md](SETUP.md#troubleshooting) for common issues and solutions.

## 📞 Support

- 📖 Check documentation files
- 🐛 Review GitHub Issues
- 💬 Start a Discussion
- 📧 Contact maintainers

---

**Happy coding! 🚀**
