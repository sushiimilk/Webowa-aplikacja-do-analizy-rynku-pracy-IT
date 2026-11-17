import type { SkillDemand } from '../types';
import { fakeRequest } from '@/lib/fake-request';
import { skillDemandMock } from './mock-data';

// TODO: gdy backend bedzie gotowy -> return apiGet<SkillDemand[]>('analytics/skills');
export function getSkillDemand(): Promise<SkillDemand[]> {
    return fakeRequest(skillDemandMock);
}
