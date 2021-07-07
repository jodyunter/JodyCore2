import React from 'react';
import axios, { AxiosResponse } from 'axios'

interface IFormProps {
    addTeam: (team: ITeam | any) => void
}
interface IFormState {
    newName: string,
    newSkill: number
}

export class TeamCreator extends React.Component<IFormProps, IFormState> {
    constructor(props: IFormProps) {
        super(props);
        this.setState({ newName: "New Name", newSkill: 0 })

        this.handleChangeName = this.handleChangeName.bind(this);
        this.handleChangeSkill = this.handleChangeSkill.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChangeName = (e: React.ChangeEvent<HTMLInputElement>) => {
        this.setState({ newName: e.currentTarget.value })
    }

    handleChangeSkill = (e: React.ChangeEvent<HTMLInputElement>) => {
        this.setState({ newSkill: parseInt(e.currentTarget.value) })
    }

    handleSubmit(e: React.SyntheticEvent) {
        e.preventDefault();
        if (this.state && this.state.newName && this.state.newSkill) {
            //this.createTeam(this.state.newName, this.state.newSkill)
            this.props.addTeam({ identifier: "", name: this.state.newName, skill: this.state.newSkill })
        }
        else {
            alert('No name and/or skill')
        }
    }

    createTeam = (name: string, skill: number) => {
        axios.post("https://localhost:5000/api/Team/create", null, {
            params: {
                name: name,
                skill: skill
            }
        }).then((response: AxiosResponse) => {
            //parent.getTeams somehow
        })
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <div className="form-group">
                    <label htmlFor="newName">Name</label>
                    <input type="text" onChange={this.handleChangeName} className="form-control" id="newName" placeholder="New Name" />
                </div>
                <div className="form-group">
                    <label htmlFor="newSkill">Skill</label>
                    <input type="text" onChange={this.handleChangeSkill} className="form-control" id="newSkill" placeholder="Skill" />
                </div>
                <button type="submit" className="btn btn-primary">Create Team</button>
            </form >
        )
    }
}
