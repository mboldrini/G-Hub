import { defineConfig } from 'vitest/config';

export default defineConfig({
  test: {
    environment: 'jsdom',
    globals: true,
    setupFiles: ['./Tests/Web/setup.js'],
    exclude: [
      '**/node_modules/**',
      '**/publish/**',
      '**/out/**',
      '**/releases/**',
      '**/.{idea,git,cache,output,temp}/**'
    ],
    coverage: {
      provider: 'v8',
      reporter: ['text', 'json', 'html'],
      exclude: [
        'node_modules/',
        'Tests/',
        '*.config.js',
        'out/',
        'publish/',
        'releases/'
      ]
    }
  }
});
