import { selectTeamById } from './teamSlice'

export const TeamPage = ({ match }) => {
    const { teamIdentifier } = match.params
    const post = useSelector(state => selectTeamById(state, teamIdentifier))
}