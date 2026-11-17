import { useState, type ChangeEvent, type FormEvent } from 'react';

const ALLOWED_EXTENSIONS = ['.pdf', '.docx', '.txt'];
const MAX_SIZE_MB = 5;

// Zwraca tresc bledu albo null, gdy plik jest w porzadku.
function validateFile(file: File): string | null {
    const name = file.name.toLowerCase();

    if (!ALLOWED_EXTENSIONS.some((ext) => name.endsWith(ext))) {
        return `Nieobsługiwany format. Dozwolone: ${ALLOWED_EXTENSIONS.join(', ')}.`;
    }
    if (file.size > MAX_SIZE_MB * 1024 * 1024) {
        return `Plik jest za duży (maks. ${MAX_SIZE_MB} MB).`;
    }
    return null;
}

type CvUploadFormProps = {
    isSubmitting: boolean;
    onSubmit: (file: File) => void;
};

export function CvUploadForm({ isSubmitting, onSubmit }: CvUploadFormProps) {
    const [file, setFile] = useState<File | null>(null);
    const [error, setError] = useState<string | null>(null);

    function handleFileChange(event: ChangeEvent<HTMLInputElement>) {
        const selected = event.target.files?.[0] ?? null;
        const validationError = selected ? validateFile(selected) : null;

        setError(validationError);
        setFile(validationError ? null : selected);
    }

    function handleSubmit(event: FormEvent<HTMLFormElement>) {
        event.preventDefault();
        if (file) onSubmit(file);
    }

    return (
        <form className="cv-upload" onSubmit={handleSubmit}>
            <label className="cv-upload__field">
                <span>Plik CV ({ALLOWED_EXTENSIONS.join(', ')}, maks. {MAX_SIZE_MB} MB)</span>
                <input type="file" accept={ALLOWED_EXTENSIONS.join(',')} onChange={handleFileChange} />
            </label>

            {error && <p className="cv-upload__error">{error}</p>}

            <button type="submit" className="button button--primary" disabled={!file || isSubmitting}>
                {isSubmitting ? 'Analizuję...' : 'Analizuj CV'}
            </button>
        </form>
    );
}
