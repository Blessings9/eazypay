import { defineConfig } from 'vite'
import tailwindcss from '@tailwindcss/vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [
    react(),
    tailwindcss(),
  ],
  server: {
    allowedHosts: [
      'eazypay.infrasensei.com',
    ],
    // optionally also specify host and port if you're proxying or running externally
    host: '0.0.0.0',
    port: 5173,
  },
})
