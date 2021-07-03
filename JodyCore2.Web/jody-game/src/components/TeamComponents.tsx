import React, { useState } from 'react';

interface ListProps {
  teams: Team[];
}
interface Props {
  team: Team;
}

function TeamPage() {
  const [teams, setTeams] = useState([]);

  function fetchTeamshandler() {
    fetch('https://localhost:5000/api/Team/all').then(response => {
      return response.json();
    }).then((data) => {
      setTeams(data);
    });

  }

  return (

    <div>
      {fetchTeamshandler()}
      <TeamList teams={teams} />
    </div>
  );
}

export const TeamEdit: React.FC<Props> = ({ team }) => {
  return (
    <form>
      <div className="mb-3">
        <label htmlFor="nameInput" className="form-label">Name</label>
        <input type="text" className="form-control" id="nameInput" />
      </div>
    </form>
  );
}

export const TeamList: React.FC<ListProps> = ({ teams }) => {
  return (
    <table className="table table-striped">
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
      <td>
        {team.name}
      </td>
      <td className="col-sm">
        {team.skill}
      </td>
      <td className="col-sm">
        {team.identifier}
      </td>
      <td>
        <button className="btn btn-primary btn-sm">Edit</button>|
        <button className="btn btn-danger btn-sm">Delete</button>
      </td>
    </tr>

  );
};

export default TeamPage;