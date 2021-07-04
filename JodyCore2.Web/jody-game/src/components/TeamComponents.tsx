import React, { useState } from 'react';
import Select from 'react-select'

interface ListProps {
  teams: Team[];
}

interface OptionProps {
  team_options: Array<{ value: string, label: string }>;

}
interface Props {
  team: Team;
}

function TeamPage() {
  const [teams, setTeams] = useState([]);
  const [team_options, setTeamOptions] = useState<{ value: string, label: string }[]>([]);

  function fetchTeamshandler() {
    fetch('https://localhost:5000/api/Team/all').then(response => {
      return response.json();
    }).then((data) => {
      setTeams(data);

      var options = Array<{ value: string, label: string }>()

      teams.forEach((t: Team) => {
        options.push({ value: t.identifier, label: t.name });
      });
      setTeamOptions(options);
    });

  }

  return (

    <div>
      {fetchTeamshandler()}
      <TeamList teams={teams} />
      <TeamEdit team_options={team_options} />
    </div>
  );
}

export const TeamEdit: React.FC<OptionProps> = ({ team_options }) => {
  return (
    <form>
      <div className="form-row">
        <div className="form-group col-md-2">
          <label htmlFor="teamEditSelect">Select Team</label>
          <Select options={team_options} className="form-control" id="teamEditSelect" />
        </div>
      </div>
      <div className="form-row">
        <div className="form-group col-md-2">
          <label htmlFor="nameInput" className="form-label">Name</label>
          <input type="text" className="form-control" id="nameInput" />
        </div>
      </div>
      <div className="form-row">
        <div className="form-group col-md-2">
          <label htmlFor="skillInput" className="form-label">Skill</label>
          <input type="text" className="form-control" id="skillInput" />
        </div>
      </div>
      <div className="form-row">
        <div className="form-group col-md-2 text-center">
          <button className="btn btn-primary">Save</button>|
          <button className="btn btn-secondary">Undo</button>|
          <button className="btn btn-danger">Create</button>
        </div>
      </div>
    </form >
  );
}

export const TeamList: React.FC<ListProps> = ({ teams }) => {
  return (
    <table className="table table-sm table-hover">
      <thead>
        <th>Name</th>
        <th>Skill</th>
        <th>Id</th>
        <th>Action</th>
      </thead>
      <tbody>
        {teams.map((object, i) => <TeamListItem team={object} key={i} />)}
      </tbody>
    </table>
  );
}

export const TeamListItem: React.FC<Props> = ({ team }) => {
  return (
    <tr>
      <td className="col col-sm-3">
        {team.name}
      </td>
      <td className="col col-sm-1">
        {team.skill}
      </td>
      <td className="col col-sm-1">
        {team.identifier}
      </td>
      <td className="col col-sm-1">
        <button className="btn btn-primary btn-sm">Edit</button>|
        <button className="btn btn-danger btn-sm">Delete</button>
      </td>
    </tr>

  );
};

export default TeamPage;
