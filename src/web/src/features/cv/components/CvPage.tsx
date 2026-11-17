import { useState } from 'react';
import { StatusMessage } from '@/components/ui/StatusMessage';
import { analyzeCv } from '../api/analyze-cv';
import type { CvAnalysis } from '../types';
import { CvAnalysisResult } from './CvAnalysisResult';
import { CvUploadForm } from './CvUploadForm';
import './cv.css';

// Tu nie uzywamy useAsync: analiza nie startuje sama po wejsciu na strone,
// tylko po kliknieciu przycisku. Stan prowadzimy wiec recznie, ale w tych samych czterech wariantach.
type AnalysisState =
    | { status: 'idle' }
    | { status: 'loading' }
    | { status: 'error' }
    | { status: 'success'; data: CvAnalysis };

export function CvPage() {
    const [state, setState] = useState<AnalysisState>({ status: 'idle' });

    async function handleSubmit(file: File) {
        setState({ status: 'loading' });
        try {
            const data = await analyzeCv(file);
            setState({ status: 'success', data });
        } catch {
            setState({ status: 'error' });
        }
    }

    return (
        <section>
            <h1>Dopasuj CV</h1>
            <p className="cv-intro">
                Wgraj swoje CV, a pokażemy, jakie umiejętności w nim rozpoznaliśmy i które oferty pasują najlepiej.
            </p>

            <CvUploadForm isSubmitting={state.status === 'loading'} onSubmit={handleSubmit} />

            {state.status === 'loading' && <StatusMessage>Analizuję CV...</StatusMessage>}
            {state.status === 'error' && (
                <StatusMessage variant="error">Nie udało się przeanalizować CV. Spróbuj ponownie.</StatusMessage>
            )}
            {state.status === 'success' && <CvAnalysisResult analysis={state.data} />}
        </section>
    );
}
