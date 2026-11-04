import { useEffect, useState } from 'react';
import { apiGet } from './api/client';
import './App.css';

function App() {
    const [status, setStatus] = useState('łączenie...');

    useEffect(() => {
        apiGet('health')
            .then(data => setStatus(data.status))
            .catch(() => setStatus('brak połączenia z API'));
    }, []);

    return (
        <main>
            <h1>Analiza rynku pracy IT</h1>
            <p>Status backendu: {status}</p>
        </main>
    );
}

export default App;
