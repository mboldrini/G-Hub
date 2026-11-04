// Playwright Configuration for PR Dashboard E2E Tests
import { defineConfig, devices } from '@playwright/test';

export default defineConfig({
  testDir: './Tests',
  testMatch: '**/*e2e*.spec.js',

  // Maximum time one test can run
  timeout: 60 * 1000,

  // Timeout for expect() assertions
  expect: {
    timeout: 10000
  },

  // Run tests in parallel
  fullyParallel: false,

  // Fail the build on CI if you accidentally left test.only in the source code
  forbidOnly: !!process.env.CI,

  // Retry on CI only
  retries: process.env.CI ? 2 : 0,

  // Opt out of parallel tests on CI
  workers: process.env.CI ? 1 : 1,

  // Reporter to use
  reporter: [
    ['html', { outputFolder: 'Tests/playwright-report' }],
    ['list']
  ],

  // Shared settings for all the projects below
  use: {
    // Base URL to use in actions like `await page.goto('/')`
    baseURL: 'http://localhost:5555',

    // Collect trace when retrying the failed test
    trace: 'on-first-retry',

    // Screenshot on failure
    screenshot: 'only-on-failure',

    // Video on failure
    video: 'retain-on-failure',
  },

  // Configure projects for major browsers
  projects: [
    {
      name: 'chromium',
      use: { ...devices['Desktop Chrome'] },
    },
  ],

  // Run your web server before starting the tests
  // Note: This assumes the app is already running. If not, uncomment and configure webServer below.
  /*
  webServer: {
    command: 'dotnet run --project PrDashboard/PrDashboard.csproj',
    url: 'http://localhost:3000',
    reuseExistingServer: !process.env.CI,
    timeout: 120 * 1000,
  },
  */
});
