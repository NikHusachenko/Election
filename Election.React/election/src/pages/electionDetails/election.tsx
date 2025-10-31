import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { IElection, IElectionDetails } from "../../types/election";
import ElectionService from "../../services/electionService";
import { ICandidate } from "../../types/candidate";
import * as signalR from "@microsoft/signalr";

const Election = () => {
    const { id } = useParams();
    const [election, setElection] = useState<IElectionDetails | null>(null);
    const [candidates, setCandidates] = useState<ICandidate[]>(election?.candidates ?? [])

    const fetchElection = async () => {
        if (!id) {
            return;
        }

        const election : IElectionDetails = await ElectionService.getById(Number(id));

        setElection(election);
        setCandidates(election.candidates)
    }

    const handlerVote = async (candidateId : number) => {
        await ElectionService.vote(candidateId);
    }

    const subscribe = async () => {
        const connection: signalR.HubConnection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:7095/api/hubs/elections',
                { 
                    transport: signalR.HttpTransportType.WebSockets 
                })
            .withAutomaticReconnect([0, 2000, 10000, 30000])
            .build();

        connection.start().then(() => {
            candidates.forEach(c => {
                connection.invoke(`Subscribe`, c.id);

                connection.on(`handleVoteChanged_${c.id}`, (data : number) => {
                    setCandidates(candidates.map(c => {
                        if (c.id === data) {
                            c.votes++;
                        }
                        return c;
                    }))
                })
            })
        });
    }

    useEffect(() => {
        fetchElection()
    }, [id])

    useEffect(() => {
        subscribe()
    }, [election])

    return (
        <div className="container">
            <div className="card">
                <div className="card-header">
                    <label className="form-text">{election?.name}</label>
                </div>
                <div className="card-body">
                    <li className="list-group">
                        {
                            candidates.map(c => (
                                <ul className="list-group-item no-select" key={c.id}>
                                    {c.name} [{c.votes}] <span className="pointer underline" onClick={async (e) => { e.preventDefault(); await  handlerVote(c.id) }}>Vote</span>
                                </ul>
                            ))
                        }
                    </li>
                </div>
            </div>
        </div>
    )
}

export default Election;