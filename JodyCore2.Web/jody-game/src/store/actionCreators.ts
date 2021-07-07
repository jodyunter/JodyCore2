
//https://www.freecodecamp.org/news/how-to-use-redux-in-your-react-typescript-app/
import * as actionTypes from "./actionTypes"
import axios, { AxiosResponse } from 'axios'


export function addTeam(team: ITeam) {
    const action: TeamAction = {
        type: actionTypes.ADD_TEAM,
        team
    }

    return createTeamApiCall(action)
}
export function createTeamApiCall(action: TeamAction) {


    //need other work here
    axios.post("https://localhost:5000/api/Team/create", null, {
        params: {
            name: team.name,
            skill: team.skill
        }
    }).then((response: AxiosResponse) => {
        //need to put the ID on the team in the action, very important
        return (dispatch: DispatchType) => {
            setTimeout(() => {
                dispatch(action)
            })
        }
    })
}

