import { ICandidate } from "./candidate";

export interface IElection {
    id: number;
    name: string;
}

export interface IElectionDetails extends IElection {
    startedOn?: Date | null;
    endedOn?: Date | null;
    candidates: ICandidate[];
}