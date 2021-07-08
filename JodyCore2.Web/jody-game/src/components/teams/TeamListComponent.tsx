import React, { useEffect } from 'react';
import axios, { AxiosResponse } from 'axios'
import { useSelector, shallowEqual } from 'react-redux';
import { propTypes } from 'react-bootstrap/esm/Image';

//export default TeamPage;
interface IState {
  teams: ITeam[],
  status: Boolean,
  rowKey: string,
  newName: string,
  newSkill: number
}

type IProps = {
  getTeams: () => TeamAction
}

type InEditMode = {
  status: boolean,
  rowKey: string
}

type UpdateData = {
  id: string,
  newName: string,
  newSkill: number
}

export const TeamEditor: React.FC<IProps> = ({ getTeams }) => {
  const [inEditMode, setInEditMode] = React.useState<InEditMode>({ status: false, rowKey: "" })
  const [updateData, setUpdateData] = React.useState<UpdateData>({ id: "", newName: "No Name", newSkill: -1 })

  const teams: readonly ITeam[] = useSelector(
    (state: TeamState) => state.teams,
    shallowEqual
  )
  const loaded: boolean = useSelector(
    (state: TeamState) => state.loaded,
    shallowEqual
  )

  useEffect(() => {
    if (!loaded) {
      getTeams()
    }
  });

  function onEdit(id: string, name: string, skill: number) {
    setInEditMode({ status: true, rowKey: id })
    setUpdateData({ id: id, newName: name, newSkill: skill })
  }

  function onDelete(id: string) {
    if (window.confirm('Are you sure, want to delete this team?')) {
      deleteTeam(id);
    }
  }

  function onCancel() {
    // reset the inEditMode state value
    setInEditMode({ status: false, rowKey: "null" })
    setUpdateData({ id: "", newName: "", newSkill: -1 })
  }

  function onSave(id: string, name: string, skill: number) {
    updateTeam(id, name, skill);
  }

  function deleteTeam(id: string) {
    axios.post('https://localhost:5000/api/Team/delete', null, {
      params: {
        identifier: id
      }
    })
      .then((response: AxiosResponse) => {
        onCancel();
        //getTeams();
      })
  }

  function updateTeam(id: string, name: string, skill: number) {
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
        //getTeams();
      })
    //need to add error handling to all of this
  }

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
                  <input value={updateData.newName} onChange={(event) => setUpdateData({ id: team.identifier, newName: event.target.value, newSkill: updateData.newSkill })} />
                ) : (

                  team.name
                )
              }
            </td>
            <td className="col col-sm-1">
              {
                inEditMode.status && inEditMode.rowKey === team.identifier ? (
                  <input value={updateData.newSkill} onChange={(event) => setUpdateData({ id: team.identifier, newName: updateData.newName, newSkill: parseInt(event.target.value) })} />
                ) : (
                  team.skill
                )
              }
            </td>
            <td className="col col-sm-1">
              {
                inEditMode.status && inEditMode.rowKey === team.identifier ? (
                  <React.Fragment>
                    <button className={"btn btn-success"} onClick={() => onSave(team.identifier, updateData.newName, updateData.newSkill)}>
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
    </table >
  );
}


export default TeamEditor;