import React from 'react';
import axios, { AxiosResponse } from 'axios'

//export default TeamPage;
interface IState {
  teams: ITeam[],
  status: Boolean,
  rowKey: string,
  newName: string,
  newSkill: number
}

interface IProps {
}

class TeamEditor extends React.Component<IProps, IState> {

  constructor(props: IProps) {
    super(props)
    this.state = {
      teams: [],
      status: false,
      rowKey: "",
      newName: "",
      newSkill: -1
    }
  }

  componentDidMount() {
    this.getTeams()
  }

  setInEditMode(status: boolean, row: string) {
    this.setState({ status: status, rowKey: row })
  }

  setUpdateData(id: string, name: string, skill: number) {
    this.setState({ newName: name, newSkill: skill })
  }

  onEdit = (id: string, name: string, skill: number) => {
    this.setInEditMode(true, id)
    this.setUpdateData(id, name, skill)
  }

  onDelete = (id: string) => {
    if (window.confirm('Are you sure, want to delete this team?')) {
      this.deleteTeam(id);
    }
  }

  onCancel = () => {
    // reset the inEditMode state value
    this.setInEditMode(false, "null")
    this.setUpdateData("", "", -1)
  }

  onSave = (id: string, name: string, skill: number) => {
    this.updateTeam(id, name, skill);
  }

  getTeams = () => {
    axios.get('https://localhost:5000/api/Team/all').then((response: AxiosResponse) => {
      return response.data;
    }).then((data: ITeam[]) => {
      this.setState({ teams: data })
    });
  }

  deleteTeam = (id: string) => {
    axios.post('https://localhost:5000/api/Team/delete', null, {
      params: {
        identifier: id
      }
    })
      .then((response: AxiosResponse) => {
        this.onCancel();
        this.getTeams();
      })
  }

  updateTeam = (id: string, name: string, skill: number) => {
    axios.post('https://localhost:5000/api/Team/update', null, {
      params: {
        identifier: id,
        name: name,
        skill: skill
      }
    })
      .then((response: AxiosResponse) => {
        // reset inEditMode and unit price state values
        this.onCancel();
        // fetch the updated data
        this.getTeams();
      })
    //need to add error handling to all of this
  }

  render() {
    const teams = this.state.teams
    const inEditMode = { status: this.state.status, rowKey: this.state.rowKey };
    return (
      <table className="table table-sm table-hover">
        <thead>
          <tr>
            <th>Name</th>
            <th>Skill</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {teams.map((team: ITeam) => (
            <tr key={team.identifier}>
              <td className="col col-sm-2">
                {
                  inEditMode.status && inEditMode.rowKey === team.identifier ? (
                    <input value={this.state.newName} onChange={(event) => this.setUpdateData(team.identifier, event.target.value, this.state.newSkill)} />
                  ) : (

                    team.name
                  )
                }
              </td>
              <td className="col col-sm-1">
                {
                  inEditMode.status && inEditMode.rowKey === team.identifier ? (
                    <input value={this.state.newSkill} onChange={(event) => this.setUpdateData(team.identifier, this.state.newName, parseInt(event.target.value))} />
                  ) : (
                    team.skill
                  )
                }
              </td>
              <td className="col col-sm-1">
                {
                  inEditMode.status && inEditMode.rowKey === team.identifier ? (
                    <React.Fragment>
                      <button className={"btn btn-success"} onClick={() => this.onSave(team.identifier, this.state.newName, this.state.newSkill)}>
                        Save
                      </button>
                      <button className={"btn btn-secondary"} style={{ marginLeft: 8 }} onClick={() => this.onCancel()}>
                        Cancel
                      </button>
                    </React.Fragment>
                  ) : (
                    <React.Fragment>
                      <button className={"btn btn-primary"} onClick={() => this.onEdit(team.identifier, team.name, team.skill)}>
                        Edit
                      </button>
                      <button className={"btn btn-danger"} onClick={() => this.onDelete(team.identifier)}>
                        Delete
                      </button>
                    </React.Fragment>
                  )
                }
              </td>
            </tr>
          ))
          }
        </tbody>
      </table>
    );
  }
}

export default TeamEditor;