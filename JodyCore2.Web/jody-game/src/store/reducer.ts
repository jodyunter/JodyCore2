//https://www.freecodecamp.org/news/how-to-use-redux-in-your-react-typescript-app/

import * as actionTypes from "./actionTypes"

const initialState: TeamState = {
    teams: [
        {
            identifier: "",
            name: "None",
            skill: -1
        }
    ]
}

export const teamReducer = (
    state: TeamState = initialState,
    action: TeamAction
): TeamState => {
    switch (action.type) {
        case actionTypes.ADD_TEAM:
            const newTeam: ITeam = {
                identifier: action.team.identifier, //this is not being populatd with the response data yet
                name: action.team.name,
                skill: action.team.skill
            }
            return {
                ...state,
                teams: state.teams.concat(newTeam),
            }
        case actionTypes.GET_TEAMS:
            return {
                ...state,
                teams: state.teams = action.teams
            }
    }
    return state
}

