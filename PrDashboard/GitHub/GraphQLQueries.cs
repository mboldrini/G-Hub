namespace PrDashboard.GitHub;

public static class GraphQLQueries
{
    public const string MyOpenPrs = """
        query($query: String!, $cursor: String) {
          search(query: $query, type: ISSUE, first: 25, after: $cursor) {
            pageInfo { hasNextPage endCursor }
            nodes {
              ... on PullRequest {
                number
                title
                body
                url
                isDraft
                createdAt
                updatedAt
                headRefName
                baseRefName
                reviewDecision
                additions
                deletions
                changedFiles
                assignees(first: 10) {
                  nodes { login }
                }
                mergeable
                mergeStateStatus
                repository { nameWithOwner }
                latestReviews(first: 10) {
                  nodes { author { login } state submittedAt }
                }
                reviewRequests(first: 20) {
                  nodes {
                    requestedReviewer {
                      ... on User { login }
                      ... on Team { name }
                    }
                  }
                }
                commits(last: 1) {
                  nodes {
                    commit {
                      messageHeadline
                      committedDate
                      statusCheckRollup {
                        state
                        contexts(first: 30) {
                          nodes {
                            ... on CheckRun {
                              name
                              conclusion
                              detailsUrl
                            }
                            ... on StatusContext {
                              context
                              state
                              targetUrl
                            }
                          }
                        }
                      }
                    }
                  }
                }
                comments(last: 5) {
                  nodes { author { login } createdAt body }
                }
                reviews(last: 10) {
                  nodes { author { login } state submittedAt body }
                }
                reviewThreads(first: 50) {
                  nodes {
                    isResolved
                    isOutdated
                    comments(first: 1) {
                      nodes { author { login } createdAt path url }
                    }
                  }
                }
              }
            }
          }
        }
        """;

    public const string ReviewRequestedPrs = """
        query($query: String!, $cursor: String) {
          search(query: $query, type: ISSUE, first: 25, after: $cursor) {
            pageInfo { hasNextPage endCursor }
            nodes {
              ... on PullRequest {
                number
                title
                url
                isDraft
                createdAt
                updatedAt
                reviewDecision
                author { login }
                repository { nameWithOwner }
                latestReviews(first: 10) {
                  nodes { author { login } state submittedAt }
                }
                commits(last: 1) {
                  nodes {
                    commit {
                      statusCheckRollup {
                        state
                        contexts(first: 10) {
                          nodes {
                            ... on CheckRun { name conclusion }
                            ... on StatusContext { context state }
                          }
                        }
                      }
                    }
                  }
                }
                comments(last: 5) {
                  nodes { author { login } createdAt body }
                }
                reviews(last: 5) {
                  nodes { author { login } state submittedAt body }
                }
              }
            }
          }
        }
        """;

    public const string MergedPrs = """
        query($query: String!, $cursor: String) {
          search(query: $query, type: ISSUE, first: 25, after: $cursor) {
            pageInfo { hasNextPage endCursor }
            nodes {
              ... on PullRequest {
                number
                title
                url
                createdAt
                mergedAt
                repository { nameWithOwner }
              }
            }
          }
        }
        """;

    public const string BackfillMergedPrs = """
        query($query: String!, $cursor: String) {
          search(query: $query, type: ISSUE, first: 25, after: $cursor) {
            pageInfo { hasNextPage endCursor }
            nodes {
              ... on PullRequest {
                number
                title
                body
                url
                author { login }
                headRefName
                baseRefName
                additions
                deletions
                changedFiles
                createdAt
                mergedAt
                repository { nameWithOwner }
                latestReviews(first: 10) {
                  nodes { author { login } state }
                }
              }
            }
          }
        }
        """;

    public const string ClosedUnmergedPrs = """
        query($query: String!, $cursor: String) {
          search(query: $query, type: ISSUE, first: 25, after: $cursor) {
            pageInfo { hasNextPage endCursor }
            nodes {
              ... on PullRequest {
                number
                title
                url
                createdAt
                closedAt
                repository { nameWithOwner }
              }
            }
          }
        }
        """;
}
