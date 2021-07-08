import { teamUpdated, selectTeamById } from './teamSlice'

export const TeamEditForm = ({ match }) => {
    const { teamIdentifier } = match.params

    const team = useSelector(state => selectTeamById(state, teamIdentifier))

    //omit component logic return it here
}