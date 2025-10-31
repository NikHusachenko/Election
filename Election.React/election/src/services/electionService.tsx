import { IElection, IElectionDetails } from "../types/election";

const ElectionService = {
    get : () : Promise<IElection[]>  => {
        return fetch('https://localhost:7095/api/elections').then(data => data.json());
    },
    getById: (id : number) : Promise<IElectionDetails> => {
        return fetch(`https://localhost:7095/api/elections/${id}`).then(data => data.json());
    },
    vote: (candidateId: number) : Promise<Response> => {
        return fetch(`https://localhost:7095/api/candidates/${candidateId}/vote`, { method: 'post' });
    }
}

export default ElectionService;