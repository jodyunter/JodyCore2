
//https://www.freecodecamp.org/news/how-to-use-redux-in-your-react-typescript-app/
//https://redux.js.org/tutorials/essentials/part-1-overview-concepts

import * as actionTypes from "./actionTypes"
import axios, { AxiosResponse } from 'axios'

export const getTeamsAction = () => {
    const action: TeamAction = {
        type: actionTypes.GET_TEAMS,
        team: { identifier: "", name: "", skill: -1 }
    }

    axios.get('https://localhost:5000/api/Team/all').then((response: AxiosResponse) => {
        return response.data;
    }).then((data: ITeam[]) => {
        action.teams = data
    });

    return action
}
export const addTeamAction = (team: ITeam) => {
    const action: TeamAction = {
        type: actionTypes.ADD_TEAM,
        team
    }

    axios.post("https://localhost:5000/api/Team/create", null, {
        params: {
            name: team.name,
            skill: team.skill
        }
    }).then((response: AxiosResponse) => {

        action.team.identifier = response.data.identifier
    }
    )

    return action
}