import React from 'react';

interface ListProps {
  teams: Team[];
}
interface Props {
  team: Team;
}

export const TeamList: React.FC<ListProps> = ({ teams }) => {
  return (
    <ul>
      {teams.map((object, i) => <TeamListItem team={object} key={i} />)}
    </ul>
  );
}

export const TeamListItem: React.FC<Props> = ({ team }) => {
  return (
  <li>
    <label>{ team.name }</label><label>{ team.skill }</label>
  </li>
  );
};

async function getTeams(): Promise<Team[]> {

  // For now, consider the data is stored on a static `users.json` file
  return fetch('https://localhost:5000/api/Team/all')
          // the JSON body is taken from the response
          .then(res => res.json())
          .then(res => {
                  // The response has an `any` type, so we need to cast
                  // it to the `User` type, and return it from the promise
                  return res as Team[]
          })
}

export default getTeams