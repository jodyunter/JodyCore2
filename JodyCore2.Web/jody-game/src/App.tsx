import React, { useState } from 'react';
import { TeamList } from './components/TeamComponents';
import TeamPage from './components/TeamComponents';

function App() {
  const [teams, setTeams] = useState([]);

  function fetchTeamshandler() {
    fetch('https://localhost:5000/api/Team/all').then(response => {
      return response.json();
    }).then((data) => {
      setTeams(data);
    });

  }

  return <TeamPage />
  /*
  return (
    <div>
      <h1>Team List</h1>
      <div>
        <h2>My Title</h2>
        <div><button> Delete</button></div>
      </div>
      <section>
        <button onClick={fetchTeamshandler}>Get Teams</button>
      </section>
      <TeamList teams={teams} />
    </div>

  );
*/
}

export default App;