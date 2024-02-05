import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import reportWebVitals from './reportWebVitals';
import { MovieRoute } from './routes/movie';
import { movieLoader } from './routes/movie/movieLoader';
import { RootLayout } from './routes/rootLayout';
import { MoviesRoute } from './routes/movies';
import { moviesLoader } from './routes/movies/moviesLoader';
import { ActorsRoute } from './routes/actors';
import { actorsLoader } from './routes/actors/actorsLoader';
import { ActorRoute } from './routes/actor';
import { actorLoader } from './routes/actor/actorLoader';
import ErrorPage from './routes/errorPage';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

const router = createBrowserRouter([
  {
    path: '/',
    element: <RootLayout />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: '/',
        element: <MoviesRoute />,
        loader: moviesLoader,
      },
      {
        path: '/movies',
        element: <MoviesRoute />,
        loader: moviesLoader,
      },
      {
        path: '/movies/:movieId',
        element: <MovieRoute />,
        loader: movieLoader,
      },
      {
        path: '/actors',
        element: <ActorsRoute />,
        loader: actorsLoader,
      },
      {
        path: '/actors/:actorId',
        element: <ActorRoute />,
        loader: actorLoader,
      }
    ],
  },
]);

root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
