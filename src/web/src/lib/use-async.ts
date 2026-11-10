import { useEffect, useState } from 'react';

type AsyncState<T> =
    | { status: 'loading' }
    | { status: 'success'; data: T }
    | { status: 'error'; error: unknown };

export function useAsync<T>(load: () => Promise<T>, deps: readonly unknown[]): AsyncState<T> {
    const key = JSON.stringify(deps);
    const [result, setResult] = useState<{ key: string; state: AsyncState<T> } | null>(null);

    useEffect(() => {
        let cancelled = false;

        load()
            .then((data) => {
                if (!cancelled) setResult({ key, state: { status: 'success', data } });
            })
            .catch((error: unknown) => {
                if (!cancelled) setResult({ key, state: { status: 'error', error } });
            });

        return () => {
            cancelled = true;
        };
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [key]);

    return result?.key === key ? result.state : { status: 'loading' };
}
