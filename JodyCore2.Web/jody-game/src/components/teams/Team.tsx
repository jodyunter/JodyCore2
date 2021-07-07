import React from 'react';
import TeamEditor from './TeamListComponent';
import { AddTeam } from './AddTeam';
import { useSelector, shallowEqual, useDispatch } from 'react-redux';
import { Dispatch } from "redux"
import { addTeamAction, getTeamsAction } from '../../store/actionCreators';


export const TeamComponent: React.FC = () => {
    const teams: readonly ITeam[] = useSelector(
        (state: TeamState) => state.teams,
        shallowEqual
    )

    const dispatch: Dispatch<any> = useDispatch()

    const addTeam = React.useCallback((team: ITeam) => dispatch(addTeamAction(team)), [dispatch])
    const getTeams = React.useCallback(() => dispatch(getTeamsAction()), [dispatch])

    return (
        <div>
            <TeamEditor teams={getTeams().teams} />
            <AddTeam addTeam={addTeam} />
        </div>
    )

}

export default TeamComponent