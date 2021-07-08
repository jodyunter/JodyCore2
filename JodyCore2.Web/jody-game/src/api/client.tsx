import axios, { AxiosResponse } from 'axios'

//maybe move the URL up higher or something or genericize these methods
export function fetchAllTeams() {
    axios.get('https://localhost:5000/api/Team/all').then((response: AxiosResponse) => {
        return response
    })
}