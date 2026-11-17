import { createBrowserRouter } from 'react-router';
import { AnalyticsPage } from '@/features/analytics/components/AnalyticsPage';
import { CvPage } from '@/features/cv/components/CvPage';
import { OffersPage } from '@/features/offers/components/OffersPage';
import { Layout } from './Layout';
import { NotFoundPage } from './NotFoundPage';

export const router = createBrowserRouter([
    {
        element: <Layout />,
        children: [
            { path: '/', element: <OffersPage /> },
            { path: '/statystyki', element: <AnalyticsPage /> },
            { path: '/cv', element: <CvPage /> },
            { path: '*', element: <NotFoundPage /> },
        ],
    },
]);
