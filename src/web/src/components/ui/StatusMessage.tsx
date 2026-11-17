import type { ReactNode } from 'react';
import './ui.css';

type StatusMessageProps = {
    variant?: 'info' | 'error';
    children: ReactNode;
};

export function StatusMessage({ variant = 'info', children }: StatusMessageProps) {
    return <p className={`status-message status-message--${variant}`}>{children}</p>;
}
