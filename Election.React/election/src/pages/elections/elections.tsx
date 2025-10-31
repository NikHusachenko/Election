import { useEffect, useState } from "react";
import { IElection } from "../../types/election";
import ElectionService from "../../services/electionService";

const Elections = () => {
    const [elections, setElections] = useState<IElection[]>([])

    const fetchElections = async () => {
        const elections: IElection[] = await ElectionService.get();
        setElections(elections)
    }

    useEffect(() => {
        fetchElections()
    }, [])

    return (
        <div className="container">
            <div className="card">
                <div className="card-body">
                    <li className="list-group">
                        {
                            elections.map(e => (
                                <ul className="list-group-item" key={e.id}>
                                    <a href={`/elections/${e.id}`} >{e.name}</a>
                                </ul>
                            ))
                        }
                    </li>
                </div>
            </div>
        </div>
    )
}

export default Elections;