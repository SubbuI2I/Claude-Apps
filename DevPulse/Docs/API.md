# API Documentation

## Authentication

### POST /api/auth/register
Register a new user.

Request body:
```json
{
  "email": "user@example.com",
  "password": "SecureP@ssw0rd",
  "fullName": "Developer User"
}
```

Response:
```json
{
  "token": "<jwt-token>",
  "message": "Registration successful"
}
```

### POST /api/auth/login
Authenticate an existing user.

Request body:
```json
{
  "email": "user@example.com",
  "password": "SecureP@ssw0rd"
}
```

Response:
```json
{
  "token": "<jwt-token>",
  "message": "Login successful"
}
```

### GET /api/auth/validate
Validate an existing JWT token.

Headers:
```http
Authorization: Bearer <jwt-token>
```

Response:
```json
{
  "userId": "<user-guid>",
  "email": "user@example.com",
  "isValid": true
}
```

## Repository Endpoints

### GET /api/repositories
List connected repositories for the signed-in user.

Response:
```json
[
  {
    "id": "<repo-guid>",
    "name": "repo-name",
    "owner": "owner-name",
    "repositoryUrl": "https://github.com/owner-name/repo-name",
    "connectedAt": "2026-06-08T00:00:00Z"
  }
]
```

### POST /api/repositories/connect
Connect a GitHub repository to the user account.

Request body:
```json
{
  "owner": "owner-name",
  "name": "repo-name"
}
```

Response:
```json
{
  "message": "Repository connected successfully",
  "repository": { ... }
}
```

### GET /api/repositories/{repoId}
Get repository details and related metrics.

Response:
```json
{
  "id": "<repo-guid>",
  "name": "repo-name",
  "owner": "owner-name",
  "metrics": [ ... ]
}
```

### DELETE /api/repositories/{repoId}
Disconnect a repository from the account.

Response:
```json
{
  "message": "Repository disconnected successfully"
}
```

## Metrics Endpoints

### GET /api/metrics/dashboard
Return aggregated dashboard metrics for the signed-in user.

Response:
```json
{
  "totalRepositories": 3,
  "totalCommits": 125,
  "totalPullRequests": 42,
  "totalIssues": 18,
  "totalStars": 73
}
```

### GET /api/metrics/{repoId}
Get repository metrics by date range.

Query parameters:
- `from` (optional) ISO 8601 timestamp
- `to` (optional) ISO 8601 timestamp

Response:
```json
[
  {
    "id": "<metric-guid>",
    "commitCount": 12,
    "pullRequestCount": 4,
    "issueCount": 1,
    "starCount": 2,
    "forksCount": 0,
    "averagePRMergeTimeHours": 3.5,
    "recordedAt": "2026-06-07T00:00:00Z"
  }
]
```

### GET /api/metrics/{repoId}/latest
Get the most recent metric entry for a repository.

Response:
```json
{
  "id": "<metric-guid>",
  "commitCount": 12,
  "pullRequestCount": 4,
  "issueCount": 1,
  "starCount": 2,
  "forksCount": 0,
  "averagePRMergeTimeHours": 3.5,
  "recordedAt": "2026-06-07T00:00:00Z"
}
```

### POST /api/metrics/{repoId}
Record new repository metrics.

Request body:
```json
{
  "commitCount": 12,
  "pullRequestCount": 4,
  "issueCount": 1,
  "starCount": 2,
  "forksCount": 0,
  "averagePRMergeTimeHours": 3.5
}
```

Response:
```json
{
  "message": "Metrics recorded successfully",
  "metric": { ... }
}
```
