import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import { fetchAllTeams } from '../api/client'

const initialState = {
    teams: [],
    status: 'idle',
    error: null
}

export const fetchTeams = createAsyncThunk('Team/all', async () => {
    const response = await fetchAllTeams()
})

const teamSlice = createSlice(
    {
        name: 'teams',
        initialState,
        reducers: {
            teamAdded: {
                reducer(state, action) {
                    state.teams.push(action.payload)
                },
                prepare(identifier, name, skill) {

                }
            }
        },
        teamUpdated(state, action) {
            const { identifier, name, skill } = action.payload
            const existingTeam = state.teams.find(team => team.identifier == identifier)
            if (existingTeam) {
                existingTeam.name = name
                existingTeam.skill = skill
            }
        }
    }
)

export const { teamAdded, teamUpdated, teamDeleted } = teamSlice.actions

export default teamSlice.reducer

export const selectAllTeams = state => state.teams

export const selectPostById = (state, teamIdentifier) =>
    state.teams.find(team => team.identifier == teamIdentifier)