import { fakeRequest } from '@/lib/fake-request';
import type { CvAnalysis } from '../types';
import { cvAnalysisMock } from './mock-data';

// TODO: gdy backend bedzie gotowy -> wyslac plik jako multipart/form-data:
//   const body = new FormData();
//   body.append('file', file);
//   return apiPost<CvAnalysis>('cv/analyze', body);
// (apiPost trzeba wtedy dopisac do lib/api-client.ts)
export function analyzeCv(file: File): Promise<CvAnalysis> {
    void file; // mock nie czyta pliku
    return fakeRequest(cvAnalysisMock, 1200);
}
