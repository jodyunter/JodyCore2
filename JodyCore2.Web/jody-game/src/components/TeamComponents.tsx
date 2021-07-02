import React from 'react';

interface ListProps {
  teams: Team[];
}
interface Props {
  team: Team;
}

export const TeamList: React.FC<ListProps> = ({ teams }) => {
  return (
    <div className="container">      
          {teams.map((object, i) => <TeamListItem team={object} key={i} />)}
    </div>    
  );
}

export const TeamListItem: React.FC<Props> = ({ team }) => {
  return (  
    <div className="row">
      <div className="col-sm">
        { team.name }    
      </div>    
      <div className="col-sm">
        { team.skill }
      </div>
      <div className="col-sm">
        { team.identifier }
      </div>
    </div>
  
  );
};