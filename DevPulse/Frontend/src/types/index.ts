export interface User {
  id: string;
  email: string;
  fullName?: string;
}

export interface Repository {
  id: string;
  userId: string;
  name: string;
  owner: string;
  repositoryUrl: string;
  gitHubId: number;
  description?: string;
  connectedAt: string;
  lastSyncedAt: string;
}

export interface Metric {
  id: string;
  repositoryId: string;
  commitCount: number;
  pullRequestCount: number;
  issueCount: number;
  starCount: number;
  forksCount: number;
  averagePRMergeTimeHours?: number;
  recordedAt: string;
  periodStart: string;
  periodEnd: string;
}

export interface DashboardMetrics {
  totalRepositories: number;
  totalCommits: number;
  totalPullRequests: number;
  totalIssues: number;
  totalStars: number;
}
