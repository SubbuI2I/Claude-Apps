## DevPulse Architecture & File Structure

### Backend Architecture

**Pattern:** Layered Architecture (API → Core → Data)

```
DevPulse.API (Presentation Layer)
├── Controllers/
│   ├── AuthController        # Authentication endpoints
│   ├── RepositoriesController # Repository management
│   └── MetricsController     # Metrics endpoints
├── Models/                    # Request/Response DTOs
├── Program.cs                 # Application setup
├── appsettings.json          # Configuration

DevPulse.Core (Business Logic Layer)
├── Services/
│   ├── IAuthService          # Authentication logic
│   ├── IRepositoryService    # Repository operations
│   ├── IMetricsService       # Metrics calculation
│   └── IGitHubService        # GitHub API integration

DevPulse.Data (Data Access Layer)
├── Models/                    # Entity models
├── DevPulseContext.cs        # EF Core DbContext
└── Migrations/               # Database migrations
```

### Frontend Architecture

**Pattern:** Component-Based Architecture

```
Frontend/
├── src/
│   ├── components/
│   │   ├── ProtectedRoute.tsx    # Auth wrapper
│   │   ├── MetricsChart.tsx      # Recharts component
│   │   └── RepositoryCard.tsx    # Repo display
│   ├── pages/
│   │   ├── Login.tsx             # Auth page
│   │   └── Dashboard.tsx         # Main dashboard
│   ├── services/
│   │   ├── api.ts                # Axios client
│   │   ├── authService.ts        # Auth API calls
│   │   ├── repositoryService.ts  # Repo API calls
│   │   └── metricsService.ts     # Metrics API calls
│   ├── hooks/
│   │   └── useAuth.ts            # Auth hook
│   ├── types/
│   │   └── index.ts              # TypeScript interfaces
│   ├── App.tsx                   # Main App component
│   └── main.tsx                  # Entry point
```

### Database Schema

**Tables:**

1. **Users**
   - id (PK, GUID)
   - email (UNIQUE)
   - passwordHash
   - fullName
   - createdAt
   - lastLoginAt

2. **Repositories**
   - id (PK, GUID)
   - userId (FK → Users)
   - name
   - owner
   - repositoryUrl
   - gitHubId (UNIQUE per user)
   - description
   - connectedAt
   - lastSyncedAt

3. **Metrics**
   - id (PK, GUID)
   - repositoryId (FK → Repositories)
   - commitCount
   - pullRequestCount
   - issueCount
   - starCount
   - forksCount
   - averagePRMergeTimeHours
   - recordedAt
   - periodStart
   - periodEnd

4. **Sessions**
   - id (PK, GUID)
   - userId (FK → Users)
   - token (UNIQUE)
   - createdAt
   - expiresAt
   - revokedAt

5. **ActivityLogs**
   - id (PK, GUID)
   - repositoryId (FK → Repositories)
   - activityType (commit/pr/issue)
   - author
   - title
   - description
   - externalUrl
   - occurredAt
   - recordedAt

### API Flow

1. **Authentication**
   - User → POST /auth/register/login → JWT Token
   - Token stored in localStorage

2. **Repository Connection**
   - Frontend → GET /repositories
   - Frontend → POST /repositories/connect (GitHub repo details)
   - Backend validates with GitHub API
   - Stores in database

3. **Metrics Collection**
   - Backend can call GitHub API for latest metrics
   - POST /metrics/{repoId} records snapshot
   - GET /metrics/dashboard for aggregated stats

4. **Data Sync**
   - Webhook or scheduled job pulls latest GitHub data
   - Records metrics in time-series table
   - Frontend displays trends

### Technology Dependencies

**Backend (.NET 8)**
- EntityFrameworkCore (ORM)
- Npgsql (PostgreSQL driver)
- Octokit (GitHub API)
- JWT Bearer (Authentication)
- Swagger (API documentation)

**Frontend**
- React 18 (UI)
- React Router (Navigation)
- Axios (HTTP)
- Recharts (Charts)
- Tailwind CSS (Styling)
- TypeScript (Type safety)

### Key Design Patterns

1. **Dependency Injection** - Services injected via constructor
2. **Repository Pattern** - Data access abstraction
3. **Service Layer** - Business logic separation
4. **Async/Await** - Non-blocking operations
5. **JWT Tokens** - Stateless authentication
6. **CORS** - Cross-origin security
7. **Environment Variables** - Configuration management

### Deployment Architecture

```
Docker Compose
├── Frontend Container (Nginx)
│   └── React SPA
├── Backend Container (.NET)
│   └── ASP.NET Core API
└── PostgreSQL Container
    └── Database
```

Each container communicates via Docker network, exposed ports for external access.
