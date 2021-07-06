import React from 'react';
import axios, { AxiosResponse } from 'axios'

class TeamEditor extends React.Component {
    constructor(props) {
        super(props);
        this.state = { value: '' };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(e: React.FormEvent<HTMLFormElement>) {
        this.setState({ value: e.target.value });
    }

    handleSubmit(event) {
        alert('A name was submitted: ' + this.state.value);
        event.preventDefault();
    }

    render() {
        return (
            <form>
                <div className="form-group">
                    <label htmlFor="newName">Name</label>
                    <input type="text" className="form-control" id="newName" placeholder="New Name" />
                </div>
                <div className="form-group">
                    <label htmlFor="newSkill">Skill</label>
                    <input type="text" className="form-control" id="newSkill" placeholder="Skill" />
                </div>
                <button type="submit" className="btn btn-primary">Create Team</button>
            </form >
        )
    }
}