import React from 'react';
import TeamEditor from './TeamListComponent';
import { TeamCreator } from './TeamCreateComponent';
import { useSelector, shallowEqual, useDispatch } from 'react-redux';
import { Dispatch } from "redux"
import { createTeam } from '../../store/actionCreators';


export const TeamComponent: React.FC = () => {
    const teams: readonly ITeam[] = useSelector(
        (state: TeamState) => state.teams,
        shallowEqual
    )

    const dispatch: Dispatch<any> = useDispatch()

    const addTeam = React.useCallback((team: ITeam) => dispatch(createTeam(team)), [dispatch])

    return (
        <div>
            <TeamEditor />
            <TeamCreator addTeam={addTeam} />
        </div>
    )

}

export default TeamComponent