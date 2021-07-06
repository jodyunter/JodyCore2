import React, { useState, useEffect } from 'react';
import axios, { AxiosResponse } from 'axios'

function TeamPage() {
  const [teams, setTeams] = useState<Team[]>([]);

  const [inEditMode, setInEditMode] = useState<{ status: Boolean, rowKey: string }>({
    status: false,
    rowKey: ""
  });

  const [newName, setNewName] = useState<string>("")
  const [newSkill, setNewSkill] = useState<number>(-1)

  const onEdit = (id: string, name: string, skill: number) => {
    setInEditMode({
      status: true,
      rowKey: id
    })
    setNewName(name)
    setNewSkill(skill)
  }

  function fetchTeamshandler() {
    axios.get('https://localhost:5000/api/Team/all').then((response: AxiosResponse) => {
      return response.data;
    }).then((data: Team[]) => {
      setTeams(data);
    });

  }

  const updateTeam = (id: string, name: string, skill: number) => {
    axios.post('https://localhost:5000/api/Team/update', null, {
      params: {
        identifier: id,
        name: name,
        skill: skill
      }
    })
      .then((response: AxiosResponse) => {

        // reset inEditMode and unit price state values
        onCancel();

        // fetch the updated data
        fetchTeamshandler();
      })
  }

  const deleteTeam = (id: string) => {
    axios.post('https://localhost:5000/api/Team/delete', null, {
      params: {
        identifier: id
      }
    })
      .then((response: AxiosResponse) => {
        onCancel();
        fetchTeamshandler();
      })
  }

  const onDelete = (id: string) => {
    if (window.confirm('Are you sure, want to delete this team?')) {
      deleteTeam(id);
    }
  }

  const onSave = (id: string, name: string, skill: number) => {
    updateTeam(id, name, skill);
  }

  const onCancel = () => {
    // reset the inEditMode state value
    setInEditMode({
      status: false,
      rowKey: "null"
    })
    // reset the unit price state value
    setNewSkill(0)
    setNewName("")
  }

  useEffect(() => {
    fetchTeamshandler();
  }, []);

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
        {teams.map((team: Team) => (
          <tr key={team.identifier}>
            <td className="col col-sm-3">
              {
                inEditMode.status && inEditMode.rowKey == team.identifier ? (
                  <input value={newName} onChange={(event) => setNewName(event.target.value)} />
                ) : (

                  team.name
                )
              }
            </td>
            <td className="col col-sm-1">
              {
                inEditMode.status && inEditMode.rowKey == team.identifier ? (
                  <input value={newSkill} onChange={(event) => setNewSkill(parseInt(event.target.value))} />
                ) : (

                  team.skill
                )
              }
            </td>
            <td className="col col-sm-1">
              {
                inEditMode.status && inEditMode.rowKey === team.identifier ? (
                  <React.Fragment>
                    <button className={"btn btn-success"} onClick={() => onSave(team.identifier, newName, newSkill)}>
                      Save
                    </button>
                    <button className={"btn btn-secondary"} style={{ marginLeft: 8 }} onClick={() => onCancel()}>
                      Cancel
                    </button>
                  </React.Fragment>
                ) : (
                  <React.Fragment>
                    <button className={"btn btn-primary"} onClick={() => onEdit(team.identifier, team.name, team.skill)}>
                      Edit
                    </button>
                    <button className={"btn btn-danger"} onClick={() => onDelete(team.identifier)}>
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

export default TeamPage;
