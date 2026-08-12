import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import AppRoutes from './routes';
import { ToastContainer } from 'react-toastify';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <AppRoutes />
    <ToastContainer
      position="top-right"
      autoClose={3000}
      theme="dark"
      toastClassName={() =>
        "relative flex p-4 min-h-16 rounded-md justify-between overflow-hidden cursor-pointer bg-[#03346E] text-[#E2E2B6] shadow-lg mb-2 border border-[#6EACDA]/30"
      }
      progressClassName="bg-[#6EACDA]"
    />
  </React.StrictMode>
);
