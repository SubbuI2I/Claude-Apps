# API Testing Guide

## Authentication Endpoints

### Register
```bash
curl -X POST https://localhost:7294/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "password": "SecurePassword123!",
    "fullName": "John Doe"
  }'
```

Response:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIs...",
  "message": "Registration successful"
}
```

### Login
```bash
curl -X POST https://localhost:7294/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "password": "SecurePassword123!"
  }'
```

### Validate Token
```bash
curl -X GET https://localhost:7294/api/auth/validate \
  -H "Authorization: Bearer YOUR_TOKEN"
```

## Repository Endpoints

### Get Repositories
```bash
curl -X GET https://localhost:7294/api/repositories \
  -H "Authorization: Bearer YOUR_TOKEN"
```

### Connect Repository
```bash
curl -X POST https://localhost:7294/api/repositories/connect \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -d '{
    "owner": "microsoft",
    "name": "vscode"
  }'
```

### Get Repository Details
```bash
curl -X GET https://localhost:7294/api/repositories/{repoId} \
  -H "Authorization: Bearer YOUR_TOKEN"
```

### Disconnect Repository
```bash
curl -X DELETE https://localhost:7294/api/repositories/{repoId} \
  -H "Authorization: Bearer YOUR_TOKEN"
```

## Metrics Endpoints

### Get Dashboard Metrics
```bash
curl -X GET https://localhost:7294/api/metrics/dashboard \
  -H "Authorization: Bearer YOUR_TOKEN"
```

Response:
```json
{
  "totalRepositories": 5,
  "totalCommits": 1250,
  "totalPullRequests": 45,
  "totalIssues": 12,
  "totalStars": 340
}
```

### Get Repository Metrics
```bash
curl -X GET "https://localhost:7294/api/metrics/{repoId}?from=2024-01-01&to=2024-02-01" \
  -H "Authorization: Bearer YOUR_TOKEN"
```

### Get Latest Metric
```bash
curl -X GET https://localhost:7294/api/metrics/{repoId}/latest \
  -H "Authorization: Bearer YOUR_TOKEN"
```

### Record Metrics
```bash
curl -X POST https://localhost:7294/api/metrics/{repoId} \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -d '{
    "commitCount": 42,
    "pullRequestCount": 5,
    "issueCount": 3,
    "starCount": 120,
    "forksCount": 8,
    "averagePRMergeTimeHours": 2.5
  }'
```

## Using Postman

1. Import the following environment variables:
```
{
  "base_url": "https://localhost:7294",
  "token": "YOUR_JWT_TOKEN"
}
```

2. Set up requests with:
   - Headers: `Authorization: Bearer {{token}}`
   - Base URL: `{{base_url}}/api`

## Swagger UI

Access the interactive API documentation:
- Development: https://localhost:7294/swagger

## Testing with Insomnia/Postman Collection

```json
{
  "client": "Insomnia",
  "clientVersion": "2023.5.8",
  "resources": [
    {
      "_id": "req_auth_register",
      "name": "Register User",
      "url": "{{ base_url }}/auth/register",
      "method": "POST"
    }
  ]
}
```

## Common Response Codes

| Code | Meaning |
|------|---------|
| 200 | Success |
| 201 | Created |
| 400 | Bad Request |
| 401 | Unauthorized |
| 403 | Forbidden |
| 404 | Not Found |
| 500 | Server Error |

## Tips

1. Always include `Content-Type: application/json` header
2. Store JWT token after login for subsequent requests
3. Check token expiration if you get 401 errors
4. Use environment variables for sensitive data
5. Test with both valid and invalid inputs
