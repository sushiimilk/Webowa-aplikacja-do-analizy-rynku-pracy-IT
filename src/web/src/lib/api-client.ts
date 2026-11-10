export class ApiError extends Error {
    readonly status: number;

    constructor(path: string, status: number) {
        super(`API ${path}: ${status}`);
        this.status = status;
    }
}

export async function apiGet<T>(path: string, params?: Record<string, string | number | undefined>): Promise<T> {
    const query = new URLSearchParams();

    for (const [key, value] of Object.entries(params ?? {})) {
        if (value !== undefined && value !== '') {
            query.append(key, String(value));
        }
    }

    const queryString = query.toString();
    const url = `/api/${path}${queryString ? `?${queryString}` : ''}`;
    const response = await fetch(url);

    if (!response.ok) {
        throw new ApiError(path, response.status);
    }

    return (await response.json()) as T;
}
