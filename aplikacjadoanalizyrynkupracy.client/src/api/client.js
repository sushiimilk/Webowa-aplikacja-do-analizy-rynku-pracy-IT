// Wspólny punkt dostępu do backendu (.NET). Wszystkie endpointy są pod /api.
export async function apiGet(path) {
    const response = await fetch(`/api/${path}`);
    if (!response.ok) {
        throw new Error(`API ${path}: ${response.status}`);
    }
    return response.json();
}
