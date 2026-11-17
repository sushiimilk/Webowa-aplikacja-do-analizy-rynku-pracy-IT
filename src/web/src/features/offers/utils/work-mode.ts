import type { WorkMode } from '../types';

export const workModeLabels: Record<WorkMode, string> = {
    Remote: 'Zdalnie',
    Hybrid: 'Hybrydowo',
    Onsite: 'Stacjonarnie',
};

export const workModes = Object.keys(workModeLabels) as WorkMode[];

// Wartosc z adresu URL jest zwyklym tekstem, wiec sprawdzamy, czy to znany tryb pracy.
export function parseWorkMode(value: string | null): WorkMode | null {
    return workModes.find((mode) => mode === value) ?? null;
}
