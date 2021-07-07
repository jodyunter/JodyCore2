import React from 'react';
import axios, { AxiosResponse } from 'axios'

type IFormProps = {
    addTeam: (team: ITeam | any) => void
}
interface IFormState {
    newName: string,
    newSkill: number
}

export const AddTeam: React.FC<IFormProps> = ({ addTeam }) => {
    const [newName, setNewName] = React.useState<string>()
    const [newSkill, setNewSkill] = React.useState<number>()

    function handleChangeName(e: React.ChangeEvent<HTMLInputElement>) {
        setNewName(e.currentTarget.value)
    }

    function handleChangeSkill(e: React.ChangeEvent<HTMLInputElement>) {
        setNewSkill(parseInt(e.currentTarget.value))
    }

    function handleSubmit(e: React.SyntheticEvent) {
        e.preventDefault();
        if (newName && newSkill) {
            //this.createTeam(this.state.newName, this.state.newSkill)
            addTeam({ identifier: "", name: newName, skill: newSkill })
        }
        else {
            alert('No name and/or skill')
        }
    }

    return (
        <form onSubmit={handleSubmit}>
            <div className="form-group">
                <label htmlFor="newName">Name</label>
                <input type="text" onChange={handleChangeName} className="form-control" id="newName" placeholder="New Name" />
            </div>
            <div className="form-group">
                <label htmlFor="newSkill">Skill</label>
                <input type="text" onChange={handleChangeSkill} className="form-control" id="newSkill" placeholder="Skill" />
            </div>
            <button type="submit" className="btn btn-primary">Create Team</button>
        </form >
    )
}