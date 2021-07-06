import React from 'react';
import TeamEditor from './TeamListComponent';
import { TeamCreator } from './TeamCreateComponent';


interface IProps {

}

interface IState {

}

class TeamComponent extends React.Component<IProps, IState> {

    render() {
        return (
            <div>
                <TeamEditor />
                <TeamCreator />
            </div>
        )
    }
}

export default TeamComponent