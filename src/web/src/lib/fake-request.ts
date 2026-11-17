// Udaje zapytanie do backendu: zwraca dane po krotkiej chwili.
// Uzywane przez funkcje API, ktorych endpointy jeszcze nie istnieja (mocki).
export function fakeRequest<T>(data: T, ms = 500): Promise<T> {
    return new Promise((resolve) => setTimeout(() => resolve(data), ms));
}
