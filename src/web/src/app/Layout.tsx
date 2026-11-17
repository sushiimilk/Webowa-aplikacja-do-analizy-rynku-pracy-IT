import { NavLink, Outlet } from 'react-router';
import { BackendStatus } from '@/features/system-status/components/BackendStatus';

const links = [
    { to: '/', label: 'Oferty' },
    { to: '/statystyki', label: 'Statystyki' },
    { to: '/cv', label: 'Dopasuj CV' },
];

export function Layout() {
    return (
        <div className="layout">
            <header className="navbar">
                <span className="brand">RynekIT</span>
                <nav>
                    {links.map((link) => (
                        <NavLink key={link.to} to={link.to} end>
                            {link.label}
                        </NavLink>
                    ))}
                </nav>
            </header>

            <main className="container">
                <Outlet />
            </main>

            <footer className="footer">
                <BackendStatus />
            </footer>
        </div>
    );
}
